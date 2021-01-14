import { pointColor } from "./Colors.js"

export default {
    drawOnContext(point, context) {
        context.fillStyle = pointColor
        context.fillRect(point.x, point.y, point.size, point.size)
    }
}