import { getRouteParam } from "./helpers"

export default {
    connection: new signalR.HubConnectionBuilder().withUrl("gamehub").build(),
    async establishConnectionAndJoinGame() {
        try {
            await this.connection.start()
            return await this.joinGame()
        } catch (e) {
            console.error(e)
        }
    },
    async joinGame() {
        try {
            return await this.connection.invoke("JoinGame", getRouteParam('game'))
        } catch (e) {
            console.error(e)
        }
    },
    on(command, action) {
        return this.connection.on(command, action)
    },
    invoke(command, ...args) {
        this.connection.invoke(command, ...args)
    },
    async invokeKeyPressed(key) {
        try {
            await this.invoke("KeyPressed", key)
        } catch (e) {
            console.error(e)
        }
    },
    async invokeKeyReleased(key) {
        try {
            await this.invoke("KeyReleased", key)
        } catch (e) {
            console.error(e)
        }
    },
    getPlayerId() {
        return this.connection.connectionId
    }
}