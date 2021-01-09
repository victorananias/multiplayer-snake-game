import View from './View.js'
import Score from './Score.js'

export default {
    connection: new signalR.HubConnectionBuilder().withUrl("gamehub").build(),
    start() {
        this.connection.start()
            .then(this.registerJoinGame.bind(this))
            .catch(err => console.error(err.toString()))
    },
    registerJoinGame() {
        this.connection.invoke("JoinGame", this.getGameId())
            .catch(err => console.error(err.toString()))
    },
    registerUpdate() {
        this.connection.on("Update", ({ snakes, fruits, scoreList }) => {
            View.clear()
            View.drawSnakes(snakes)
            View.drawFruits(fruits)
            Score.update(scoreList)
        })
    },
    registerGameOver() {
        this.connection.on("Win", () => {
            document.querySelector('.game-over').classList.add('display-block', 'animation-running')
            const gameOverTextElement = document.querySelector('.game-over-text')
            gameOverTextElement.classList.add('win')
            gameOverTextElement.textContent = 'You Win'
        })

        this.connection.on("Lose", () => {
            document.querySelector('.game-over').classList.add('display-block', 'animation-running')
            const gameOverTextElement = document.querySelector('.game-over-text')
            gameOverTextElement.classList.add('lose')
            gameOverTextElement.textContent = 'You Lose'
        })
    },
    keyPressed(key) {
        this.connection.invoke("KeyPressed", key).catch(err => console.error(err.toString()))
    },
    keyReleased(key) {
        this.connection.invoke("KeyReleased", key).catch(err => console.error(err.toString()))
    },
    getGameId() {
        return new URLSearchParams(window.location.search).get('game')
    },
    getPlayerId() {
        return this.connection.connectionId
    }
}