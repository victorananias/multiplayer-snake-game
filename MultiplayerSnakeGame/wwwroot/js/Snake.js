import Connection from "./Connection.js"
import { deadColor, playerColor, enemyColor } from "./Colors.js"

export default {
    drawOnContext(snake, context) {
        context.fillStyle = this.getColorForSnake(snake)
        context.fillRect(snake.head.x, snake.head.y, snake.head.size, snake.head.size)

        for (let i = 0; i < snake.body.length; i++) {
            const piece = snake.body[i]
            context.fillRect(piece.x, piece.y, piece.size, piece.size)
        }
    },
    getColorForSnake(snake) {
        if (!snake.alive) {
            return deadColor
        } else if (snake.id == Connection.getPlayerId()) {
            return playerColor
        } else {
            return enemyColor
        }
    }
}