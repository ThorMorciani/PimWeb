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
    struct LoginValidado loginValidado;
    printf("Digite seu usuario para fazer login \n");
    fgets(login, sizeof(login), stdin);
    login[strcspn(login, "\n")] = '\0';
    printf("Digite sua senha \n");
    fgets(senha, sizeof(senha), stdin);
    senha[strcspn(senha, "\n")] = '\0';
    loginValidado = ValidaLogin(login, senha);
    system("clear");
    char** opcoesMenu = retornaOpcoesMenu(loginValidado.usuario.permissao);
    int qtdOpcoes = retornaQtdOpcoes(loginValidado.usuario.permissao);
    int opcaoEscolhida;

    for (int i = 0; i < qtdOpcoes; i++)
    {
        printf("%s\n", opcoesMenu[i]);
    }
    printf("Escolha uma das opções acima: \n");
    scanf("%d", &opcaoEscolhida);

    switch (loginValidado.usuario.permissao)
    {
    case 1:
        ManipularOpcaoSelecionadaAdmin(opcaoEscolhida);
        break;
    case 2:
        ManipularOpcaoSelecionadaGerente(opcaoEscolhida);
        break;
    case 3:
        ManipularOpcaoSelecionadaFunc(opcaoEscolhida);
        break;
    default:
        printf("Permissão desconhecida");
        break;
    }

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
