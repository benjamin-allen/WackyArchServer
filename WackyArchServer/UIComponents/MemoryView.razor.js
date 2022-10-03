var oldPCHilite = 0;
var oldAddrHilite = 0;

export function setPCPointer(programCounter) {
    let cell = document.getElementById("memory-cell-" + programCounter);
    cell.classList.add("pc-hilite");

    let oldcell = document.getElementById("memory-cell-" + oldPCHilite);
    oldcell.classList.remove("pc-hilite");
    oldPCHilite = programCounter;
}

export function setAddrPointer(addr) {
    let cell = document.getElementById("memory-cell-" + addr);
    cell.classList.add("addr-hilite");

    let oldcell = document.getElementById("memory-cell-" + oldAddrHilite);
    oldcell.classList.remove("addr-hilite");
    oldAddrHilite = addr;
}

export function editCell() {
    let result = prompt("New cell value (Hex):", "000");
    let parsed = parseInt(result, 16);
    debugger;
    if (isNaN(parsed) == false) {
        return parsed;
    }
    else {
        return 0xDEDBEEF;
    }
}