#include <stdio.h>
#include <stdlib.h>
#include <locale.h>
#include <string.h>
#include <stdbool.h>
#include "Headers/Usuario.h"
#include "Headers/Produto.h"
#include "Headers/Vendas.h"


void login() {
    char login[10];
    char senha[5];

    printf("Digite seu usuario para fazer login \n");
    scanf("%s", &login);
    printf("Digite sua senha \n");
    scanf("%s", &senha);
}
int main()
{
    setlocale(LC_ALL,"PORTUGUESE");

    printf("Seja bem-vindo! \n");
    bool adminUserExiste = validarUserAdmin();

    if (adminUserExiste)
        login();
    else
        criarUsuarioAdmin();
        
    return 0;
}
