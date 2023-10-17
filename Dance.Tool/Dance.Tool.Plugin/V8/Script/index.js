import { Host } from 'host'
import { Student } from 'Student'

let host = new Host();
let student = new Student('ZhangSan', 17);

// 显示消息框
host.showMessage(`Name: ${student.name}, Age: ${student.age}`);

// 显示通知框
host.showNotify(`Name: ${student.name}, Age: ${student.age}`);

// --------------------------------------------------------------------------------------------

/**
 * 冒泡排序
 * @param {any} arr 数组
 * @returns 排序后的结果
 */
function bubbleSort(arr) {
    for (let i = 0; i < arr.length - 1; i++) {
        for (let j = 0; j < arr.length - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                ;[arr[j], arr[j + 1]] = [arr[j + 1], arr[j]]
            }
        }
    }
}

let input = [1, 5, 3, 2, 4, 8, 9, 7, 6]
let output = [1, 5, 3, 2, 4, 8, 9, 7, 6];
// 排序
bubbleSort(output);

// 显示排序结果
host.showMessage(` input: ${input.join(",")} \n output: ${output.join(",")}`);