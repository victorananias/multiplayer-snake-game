import { fruitColor } from "./Colors.js"

export default {
    drawOnContext(fruit, context) {
        context.fillStyle = fruitColor
        context.fillRect(fruit.x, fruit.y, fruit.size, fruit.size)
    }
}