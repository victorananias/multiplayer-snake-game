import Connection from "./Connection.js"

export default {
    keys: ['w', 'a', 's', 'd', ' '],
    registerKeys(keyboard) {
        this.keys.forEach(key => {
            keyboard.onPress(key, () => Connection.keyPressed(key))
            keyboard.onRelease(key, () => Connection.keyReleased(key))
        })
    }
}