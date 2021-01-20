import { getRouteParam } from "./helpers.js"

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
    async startAction(action) {
        try {
            await this.invoke("StartAction", action)
        } catch (e) {
            console.error(e)
        }
    },
    async stopAction(action) {
        try {
            await this.invoke("StopAction", action)
        } catch (e) {
            console.error(e)
        }
    },
    getPlayerId() {
        return this.connection.connectionId
    }
}