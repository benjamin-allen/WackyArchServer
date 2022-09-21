var editor;

export function setup() {
    editor = ace.edit("ace-editor");
}

export function getText() {
    return editor.getValue()
}

export function setLinePointer(line, isError) {
    let gutterCells = document.getElementById("ace-editor").getElementsByClassName("ace_gutter-cell");
    for (var i = 0; i < gutterCells.length; i++) {
        let cell = gutterCells[i];
        cell.style.backgroundColor = ""; // clear all lines by default
        if (cell.innerText == line) {
            cell.style.backgroundColor = (isError) ? "red" : "yellow";
        }
    }
}