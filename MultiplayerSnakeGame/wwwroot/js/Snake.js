class Snake {

  constructor(snake, context) {
    Object.assign(this, snake)
    this.headColor = '#4bb84b'
    this.bodyColor = '#4bb84b'
    this.context = context
  }

  draw() {
    this.context.fillStyle = this.headColor
    this.context.fillRect(this.head.x, this.head.y, this.head.size, this.head.size)

    
    for (let i = 0; i < this.body.length; i++) {
      const piece = this.body[i]
      const lineWidth = parseInt(piece.size / 3)
      this.context.fillStyle = '#bb6dc7'
      this.context.fillRect(piece.x, piece.y, piece.size, piece.size)
      this.context.beginPath()
      this.context.strokeStyle = this.bodyColor
      this.context.lineWidth = lineWidth;
      this.context.rect(piece.x + Math.floor(lineWidth / 2), piece.y + Math.floor(lineWidth / 2), piece.size - (lineWidth), piece.size - (lineWidth))
      this.context.stroke()
    }
  }
}