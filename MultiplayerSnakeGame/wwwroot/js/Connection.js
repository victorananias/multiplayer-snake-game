import View from './View.js'

export default {
    connection: new signalR.HubConnectionBuilder().withUrl("gamehub").build(),
    start() {
        this.connection.start()
            .then(this.joinGame.bind(this))
            .catch(err => console.error(err.toString()))
    },
    joinGame(gameId) {
        this.connection.invoke("JoinGame", gameId)
            .catch(err => console.error(err.toString()))
    },
    registerUpdate() {
        this.connection.on("Update", ({ snakes, fruits, scoreList }) => {
            View.clear()
            View.drawSnakes(snakes)
            View.drawFruits(fruits)
            View.drawScoreList(scoreList)
        })
    },
    snakeColor(snake, playerId) {
        if (!snake.alive) {
            return deadColor
        } else if (!playerId) {
            return playerColor
        } else {
            return snake.id == playerId ? playerColor : enemyColor
        }
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
    keyPressed() {
        this.connection.invoke("KeyPressed", key).catch(err => console.error(err.toString()))
    },
    keyReleased(key) {
        this.connection.invoke("KeyReleased", key).catch(err => console.error(err.toString()))
    }
}