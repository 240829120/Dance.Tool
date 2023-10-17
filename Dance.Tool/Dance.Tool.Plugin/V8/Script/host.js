/**
 * 宿主对象
 */
class Host {

    /**
     * 显示消息框
     * @param {string} msg - 消息
     */
    showMessage(msg) {
        V8Host.ShowMessage(msg);
    }

    /**
     * 显示通知框
     * @param {string} msg - 消息
     */
    showNotify(msg) {
        V8Host.ShowNotify(msg);
    }
}

export { Host }