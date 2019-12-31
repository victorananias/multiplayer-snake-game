class Snake {

  constructor(snake, context) {
    Object.assign(this, snake)
    this.color = '#4bb84b'
    this.context = context
  }

  draw() {
    const body = [this.head, ...this.body]
    
    for (let i = 0; i < body.length; i++) {
      const piece = body[i]
      this.context.fillStyle = this.color
      this.context.fillRect(piece.x, piece.y, piece.size, piece.size)
    }
  }
}