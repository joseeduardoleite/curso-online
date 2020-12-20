function formFail(erro) {
    if (erro.status == 500)
        toastr.error(erro.responseJSON);
    else if (erro.status == 502)
        erro.responseJSON.forEach(function (mensagemErro) {
            toastr.error(mensagemErro);
        });
}