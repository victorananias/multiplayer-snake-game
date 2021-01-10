import Connection from "./Connection.js"

export default {
    keys: ['w', 'a', 's', 'd', ' '],
    registerKeys(keyboard) {
        this.keys.forEach(key => {
            keyboard.onPress(key, () => Connection.invokeKeyPressed(key))
            keyboard.onRelease(key, () => Connection.invokeKeyReleased(key))
        })
    }
}