import Snake from "./Snake.js"
import Fruit from "./Fruit.js"
import Score from "./Score.js"
import { backgroundColor } from "./Colors.js"
import { $, capitalize } from "./helpers.js"

const canvasProps = {
    x: 0,
    y: 0,
    width: 500,
    height: 500
}

export default {
    canvasContext: $('#game').getContext('2d'),
    onUpdate({ snakes, fruits, scoreList }) {
        this.clearCanvas()
        this.drawBackGround()
        this.drawObjectsOfType(snakes, Snake)
        this.drawObjectsOfType(fruits, Fruit)
        Score.update(scoreList)
    },
    onGameOver(result) {
        $('.game-over').classList.add('display-block', 'animation-running')
        $('.game-over-text').classList.add(result)
        $('.game-over-text').textContent = `You ${capitalize(result)}`
    },
    clearCanvas() {
        this.canvasContext.clearRect(canvasProps.x, canvasProps.y, canvasProps.width, canvasProps.height)
    },
    drawBackGround() {
        this.canvasContext.fillStyle = backgroundColor
        this.canvasContext.fillRect(canvasProps.x, canvasProps.y, canvasProps.width, canvasProps.height)
    },
    drawObjectsOfType(objects, type) {
        objects.forEach(object => type.drawOnContext(object, this.canvasContext))
    }
}