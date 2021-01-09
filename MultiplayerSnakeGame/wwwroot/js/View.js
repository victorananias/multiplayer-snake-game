import Snake from "./Snake.js"

const deadColor = 'grey'
const playerColor = '#4bb84b'
const enemyColor = 'yellow'

export default {
    canvas: document.querySelector('#game'),
    context: document.querySelector('#game').getContext('2d'),
    scoreList: [],
    clear() {
        this.clearCanvas()
        this.drawBackGround()
    },
    clearCanvas() {
        this.context.clearRect(0, 0, 500, 500)
    },
    drawBackGround() {
        this.context.fillStyle = '#2e2c2c'
        this.context.fillRect(0, 0, 500, 500)
    },
    drawSnakes(snakes) {
        snakes.forEach(s => new Snake(s, context, this.snakeColor(s, connection.connectionId)).draw())
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
    drawFruits(fruits) {
        fruits.forEach(f => new Fruit(f, context).draw())
    },
    drawScoreList(scoreList) {
        if (JSON.stringify(this.scoreList) !== JSON.stringify(scoreList)) {
            this.scoreList = scoreList
            updateScore()
        }
    },
    clearScoreList() {
        [...document.querySelectorAll('#score tbody tr')].forEach(e => e.remove())
    },
    updateScore() {
        this.clearScoreList()

        this.scoreList.forEach(s => {
            document.querySelector('#score').innerHTML += `
          <tr class="${s.snakeId === window.connectionId ? 'score-current-player ' : ''}">
            <td>${s.snakeId}</td>
            <td>${s.points}</td>
          </tr>
        `
        })
    }
}