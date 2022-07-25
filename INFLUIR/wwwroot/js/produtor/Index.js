var cpf = document.getElementById("cpf");
const btnCreateProdutor = document.querySelector(".btn-primary");
const formCreateProdutor = document.querySelector(".form-produtor");
$(function () {
    if (!cpf) {
        return
    }
    cpf.addEventListener(
        "input",
        (e) => {
            e.target.value = cpfMask(e.target.value);
        },
        false
    );
    formCreateProdutor.addEventListener("submit", (e) => {
        e.preventDefault()
        validarCPF(e)
    })
                
    function cpfMask(value) {
        return value
            .replace(/\D+/g, "")
            .replace(/(\d{3})(\d)/, "$1.$2")
            .replace(/(\d{3})(\d)/, "$1.$2")
            .replace(/(\d{3})(\d{1,2})/, "$1-$2")
            .replace(/(-\d{2})\d+?$/, "$1");
    }

    function validarCPF(e) {
        
        let cpfArray = Array.from(cpf.value)
        let calculo = 0
        let calculo2 = 0
        let primeiroDCpf = parseInt(cpfArray[12])
        let segundoDCpf = parseInt(cpfArray[13])
        const tamanho = cpfArray.length

        for (let i = 0; i < cpfArray.length; i++) {
            if (cpfArray[i] === '.' || cpfArray[i] === '-') { //remoção dos pontos e traço
                cpfArray.splice(i, 1)
            }
            cpfArray[i] = parseInt(cpfArray[i])
        }

        for (let i = 0, j = 10; i < cpfArray.length - 2; i++, j--) { //fazendo calculo para o primeiro digito
            calculo += cpfArray[i] * j
        }

        for (let i = 0, j = 11; i < cpfArray.length - 1; i++, j--) {  //fazendo calculo para o segundo digito
            calculo2 += cpfArray[i] * j
        }

        let primeiroDCalculado = (calculo * 10) % 11
        let segundoDCalculado = (calculo2 * 10) % 11

        if (primeiroDCalculado === 10) {
            primeiroDCalculado = 0
        }
        if (segundoDCalculado === 10) {
            segundoDCalculado = 0
        }
        
        if (tamanho == 14) {            
            if (primeiroDCpf == primeiroDCalculado && segundoDCpf == segundoDCalculado) {
                console.log(1)
                $("#cpf-invalid").css('display', "none");
                formCreateProdutor.submit()
            } else {
                console.log(2)
                $("#cpf-invalid").show()            

            }
        }
    }
});
