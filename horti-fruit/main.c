
#include <stdio.h>
#include <stdlib.h>
#include <locale.h>
#include <string.h>
#include <stdbool.h>
#include "Usuario.h"
#include "Produto.h"
#include "Vendas.h"


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

    /*char** opcoesMenu = retornaOpcoesMenu(loginValidado.usuario.permissao);
    int qtdOpcoes = retornaQtdOpcoes(loginValidado.usuario.permissao);
    int opcaoEscolhida;

    for (int i = 0; i < qtdOpcoes; i++)
    {
        printf("%s\n", opcoesMenu[i]);
    }
    printf("Escolha uma das op��es acima: \n");
    scanf("%d", &opcaoEscolhida);

    switch (loginValidado.usuario.permissao)
    {
    case 1:
        ManipularOpcaoSelecionada(opcaoEscolhida);
        break;
    case 2: // TO DO: ALTERAR PARA M�TODO CORRESPONDENTE DA CLASSE DO GERENTE
        ManipularOpcaoSelecionada(opcaoEscolhida);
        break;
    case 3: // TO DO: ALTERAR PARA M�TODO CORRESPONDENTE DA CLASSE DO FUNCION�RIO
        ManipularOpcaoSelecionada(opcaoEscolhida);
        break;
    default:
        break;
    }
*/
}
int main()
{
    setlocale(LC_ALL,"PORTUGUESE");

    printf("Seja bem-vindo! \n");
    /*bool adminUserExiste = validarUserAdmin();

    if (adminUserExiste)
        login();
    else
        criarUsuarioAdmin();
*/
    int x;
    printf("DESEJA CADASTRAR OU VER PRODUTOS? 1 CADASTRAR 2 VER");
    scanf("%d",&x);
    switch(x){
        case 1: cadastrarProduto();
        break;
        case 2: relatorioProdutos();
        break;
    }

    return 0;
}
