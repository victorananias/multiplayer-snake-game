import Connection from "./Connection.js"
import { deadColor, playerColor, enemyColor } from "./Colors.js"

export default {
    drawOnContext(player, context) {
        context.fillStyle = this.getColorForSnake(player)
        context.fillRect(player.head.x, player.head.y, player.head.size, player.head.size)

        for (let i = 0; i < player.body.length; i++) {
            const piece = player.body[i]
            context.fillRect(piece.x, piece.y, piece.size, piece.size)
        }
    },
    getColorForSnake(player) {
        if (!player.alive) {
            return deadColor
        } else if (player.id == Connection.getPlayerId()) {
            return playerColor
        } else {
            return enemyColor
        }
    }
}