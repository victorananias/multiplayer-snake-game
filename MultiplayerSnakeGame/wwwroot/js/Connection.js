

export default {
    connection: new signalR.HubConnectionBuilder().withUrl("gamehub").build(),
    async establishConnectionAndJoinGame() { try {
            await this.connection.start()
            return await this.joinGame()
        } catch (e) {
            console.error(e.toString())
        }
    },
    async joinGame() {
        try {
            return await this.connection.invoke("JoinGame", this.getGameId())
        } catch (e) {
            console.error(e.toString())
        }
    },
    on(command, action) {
        return this.connection.on(command, action)
    },
    async invokeKeyPressed(key) {
        try {
            await this.connection.invoke("KeyPressed", key)
        } catch (e) {
            console.error(e.toString())
        }
    },
    async invokeKeyReleased(key) {
        try {
            await this.connection.invoke("KeyReleased", key)
        } catch (e) {
            console.error(e.toString())
        }
    },
    getGameId() {
        return new URLSearchParams(window.location.search).get('game')
    },
    getPlayerId() {
        return this.connection.connectionId
    }
}