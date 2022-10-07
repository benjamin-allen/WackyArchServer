export function editCell() {
    let result = prompt("New input value (Hex):", "000");
    let parsed = parseInt(result, 16);
    if (isNaN(parsed) == false) {
        return parsed;
    }
    else {
        return 0xDEDBEEF;
    }
}