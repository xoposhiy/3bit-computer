let progInput = document.getElementsByClassName("program")[0];
let screenEl = document.getElementsByClassName("screen")[0];
let vm = {screen: [0,0,0,0,0,0,0,0], cursor:0, sel:-1};
let prog = "";
let ip = -1;

function step(){
    if (ip < 0){
        vm = {screen: [0,0,0,0,0,0,0,0], caret:0, sel:-1};
        prog = progInput.value;
        ip = 0;
    }
    if (ip >= prog.length) {
        ip = -1;
        return;
    }
    console.log(ip);
    let cmd = prog[ip];
    execute(vm, cmd);
    updateScreen(vm);
    ip++;
    setTimeout(step, 500);
}

function execute(vm, c){
    const len = vm.screen.length;
    if (c == 'R') vm.caret = (vm.caret + 1) % len;
    if (c == 'L') vm.caret = (vm.caret + len - 1) % len;
    if (c == '+')
        for (let pos of selection(vm))
            vm.screen[pos]++;
    if (c == '-')
        for (let pos of selection(vm))
            vm.screen[pos]--;
    if (c == '*')
        for (let pos of selection(vm))
            vm.screen[pos] *= 2;
    if (c == 'S') vm.sel = vm.caret;
    if (c == 'C') vm.sel = -1;
}

function selection(vm){
    if (vm.sel < 0) return [vm.caret];
    let res = [];
    let i = vm.sel;
    while (i != vm.caret){
        res.push(i);
        i = (i + 1) % vm.screen.length;
    }
    res.push(vm.caret);
    return res;
}

function getSymbol(code){
    if (code == 0) return " ";
    if (code > 26) return "▉";
    return String.fromCharCode('A'.charCodeAt(0) + code - 1);
}

function updateScreen(vm){
    let s = "";
    for(let i = 0; i < vm.screen.length; i++){
        if (i == vm.sel || vm.sel <0 && i == vm.caret) 
            s += "<span class='caret'>";
        s += "" + getSymbol(vm.screen[i]);
        if (i == vm.caret) 
            s += "</span>";
    }
    
    screenEl.innerHTML = s;
}

function start(){
    if (ip < 0)
        step();
}