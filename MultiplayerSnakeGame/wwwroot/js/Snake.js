import Connection from "./Connection.js"
import { deadColor, playerColor, enemyColor } from "./Colors.js"

export default class Snake {

    constructor(snake, context) {
        this.alive = true
        Object.assign(this, snake)
        this.context = context
    }

    draw() {
        this.context.fillStyle = this.getColor()
        this.context.fillRect(this.head.x, this.head.y, this.head.size, this.head.size)

        for (let i = 0; i < this.body.length; i++) {
            const piece = this.body[i]
            this.context.fillStyle = this.getColor()
            this.context.fillRect(piece.x, piece.y, piece.size, piece.size)
        }
    }

    getColor() {
        if (!this.alive) {
            return deadColor
        } else if (!Connection.getPlayerId()) {
            return playerColor
        } else {
            return this.id == Connection.getPlayerId() ? playerColor : enemyColor
        }
    }
}