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
        ManipularOpcaoSelecionada(opcaoEscolhida);
        break;
    case 2: // TO DO: ALTERAR PARA MÉTODO CORRESPONDENTE DA CLASSE DO GERENTE
        ManipularOpcaoSelecionada(opcaoEscolhida);
        break;
    case 3: // TO DO: ALTERAR PARA MÉTODO CORRESPONDENTE DA CLASSE DO FUNCIONÁRIO
        ManipularOpcaoSelecionada(opcaoEscolhida);
        break;
    default:
        break;
    }

}
int main()
{
    setlocale(LC_ALL,"PORTUGUESE");

    printf("Seja bem-vindo! \n");
    /* bool adminUserExiste = validarUserAdmin();

    if (adminUserExiste)
        login();
    else
        criarUsuarioAdmin();
        */
        int x;
    printf("DESEJA CADASTRAR OU VER PRODUTOS? 1- CADASTRAR 2- VER 3- VENDA 4- ADICIONAR ESTOQUE ");
    scanf("%d",&x);
    switch(x){
        case 1: cadastrarProduto();
        break;
        case 2: relatorioProdutos();
        break;
        case 3: vendaProduto();
        break;
        case 4: adicionarEstoqueProduto();
        break;
    }

    return 0;
}
