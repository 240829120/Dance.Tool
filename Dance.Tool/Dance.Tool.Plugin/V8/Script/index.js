import { Host } from 'host'
import { Student } from 'Student'

let host = new Host();
let student = new Student('ZhangSan', 17);

// 显示消息框
host.showMessage(`Name: ${student.name}, Age: ${student.age}`);

// 显示通知框
host.showNotify(`Name: ${student.name}, Age: ${student.age}`);