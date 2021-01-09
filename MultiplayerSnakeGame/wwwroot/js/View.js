import Snake from "./Snake.js"
import Fruit from "./Fruit.js"
import { backgroundColor } from "./Colors.js"

const canvas = {
    x: 0,
    y: 0,
    width: 500,
    height: 500
}

export default {
    canvas: document.querySelector('#game'),
    context: document.querySelector('#game').getContext('2d'),
    clear() {
        this.clearCanvas()
        this.drawBackGround()
    },
    clearCanvas() {
        this.context.clearRect(canvas.x, canvas.y, canvas.width, canvas.height)
    },
    drawBackGround() {
        this.context.fillStyle = backgroundColor
        this.context.fillRect(canvas.x, canvas.y, canvas.width, canvas.height)
    },
    drawSnakes(snakes) {
        snakes.forEach(snake => new Snake(snake, this.context).draw())
    },
    drawFruits(fruits) {
        fruits.forEach(fruit => new Fruit(fruit, this.context).draw())
    }
}