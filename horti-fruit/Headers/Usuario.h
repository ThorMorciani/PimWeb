#ifndef USUARIO.H
#define USUARIO.H 

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

struct Usuario {
    int id;
    char nome[20];
    char documento[20];
    int permissao;
    char login[10];
    char senha[5];
};

struct LoginValidado {
    struct Usuario usuario;
    bool validado;
};

static char* menuAdmin[] = {
        "1 - Cadastrar Usuário",
        "2 - Cadastrar Produto",
        "3 - Relatório de Usuários",
        "4 - Relatório de Vendas",
        "5 - Relatório de Produtos"
};
static char* menuGerente[] = {
        "1 - Cadastrar Produto",
        "2 - Relatório de Vendas",
        "3 - Relatório de Produtos",
        "4 - Realizar uma venda"
};

static char* menuFuncionario[] = {
        "1 - Realizar uma venda"
};

void abrirArquivoUsuarioEscrita();
void abrirArquivoUsuarioLeitura();
void fecharArquivoUsuario();
bool validarUserAdmin();
void criarUsuarioAdmin();
struct LoginValidado ValidaLogin(char login[10], char senha[10]);
void RelatorioUsuarios();
char** retornaOpcoesMenu(int permissao);
int retornaQtdOpcoes(int permissao);
void ManipularOpcaoSelecionada(int opcaoEscolhida);
#endif
