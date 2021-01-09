import { fruitColor } from "./Colors.js"

export default class Fruit {
    constructor(fruit, context) {
        Object.assign(this, fruit)
        this.context = context
    }

    draw() {
        this.context.fillStyle = fruitColor
        this.context.fillRect(this.x, this.y, this.size, this.size)
    }
}