#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include "Headers/Usuario.h"
#include "Headers/Produto.h"
#include "Headers/Vendas.h"

const char *nome_arquivo_usuario = "usuario.txt";
FILE *arquivoUsuario = NULL;

void abrirArquivoUsuarioEscrita() {
    arquivoUsuario = fopen(nome_arquivo_usuario, "a");
    if (arquivoUsuario == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void abrirArquivoUsuarioLeitura() {
    arquivoUsuario = fopen(nome_arquivo_usuario, "r");
    if (arquivoUsuario == NULL) {
        printf("O arquivo '%s' não existe. Criando o arquivo...\n", nome_arquivo_usuario);

        arquivoUsuario = fopen(nome_arquivo_usuario, "a");
        if (arquivoUsuario == NULL) {
            printf("Erro ao criar o arquivo.\n");
            return;    
        }
        fclose(arquivoUsuario);
        arquivoUsuario = fopen(nome_arquivo_usuario, "r");
        if (arquivoUsuario == NULL) {
            printf("Erro ao reabrir o arquivo em modo de leitura.\n");
            return;
        }
    }
}

void fecharArquivoUsuario() {
    if (arquivoUsuario != NULL) {
        fclose(arquivoUsuario);
        arquivoUsuario = NULL;
    }
}

bool validarUserAdmin() {
    abrirArquivoUsuarioLeitura();
    char linha[100];

    if (fgets(linha, sizeof(linha), arquivoUsuario) != NULL) {
        return true;
        fclose(arquivoUsuario);
    }
    else {
        return false;
        fclose(arquivoUsuario);
    }
}

void gravarDadosEmArquivo(struct Usuario usuario) {
    fprintf(arquivoUsuario, "\n%d,%s,%s,%d,%s,%s\n", usuario.id, usuario.nome, usuario.documento, usuario.permissao, usuario.login, usuario.senha);
}

void criarUsuarioAdmin() {
    struct Usuario usuarioAdmin;
    usuarioAdmin.id = 1;
    usuarioAdmin.permissao = 1;

    abrirArquivoUsuarioEscrita();
    printf("Parece que não existe nenhum usuário administrador cadastrado. \n");
    printf("Cadastre um administrador para efetuar o primeiro login no sistema. \n");
    printf("Digite seu nome \n");
    fgets(usuarioAdmin.nome, sizeof(usuarioAdmin.nome), stdin);
    usuarioAdmin.nome[strcspn(usuarioAdmin.nome, "\n")] = '\0';
    printf("Digite seu CPF \n");
    fgets(usuarioAdmin.documento, sizeof(usuarioAdmin.documento), stdin);
    usuarioAdmin.documento[strcspn(usuarioAdmin.documento, "\n")] = '\0';
    printf("Digite login \n");
    fgets(usuarioAdmin.login, sizeof(usuarioAdmin.login), stdin);
    usuarioAdmin.login[strcspn(usuarioAdmin.login, "\n")] = '\0';
    printf("Digite a senha \n");
    fgets(usuarioAdmin.senha, sizeof(usuarioAdmin.senha), stdin);
    usuarioAdmin.senha[strcspn(usuarioAdmin.senha, "\n")] = '\0';
    gravarDadosEmArquivo(usuarioAdmin);
    fecharArquivoUsuario();
}

struct LoginValidado ValidaLogin(char login[10], char senha[5]) {
    abrirArquivoUsuarioLeitura();
    char linha[50];
    struct LoginValidado loginValidado;

    while (!feof(arquivoUsuario))
    {
        fscanf(arquivoUsuario,"%d,%[^,],%[^,],%d,%[^,],%[^\n]",&loginValidado.usuario.id,loginValidado.usuario.nome, loginValidado.usuario.documento, &loginValidado.usuario.permissao, loginValidado.usuario.login, loginValidado.usuario.senha);
        if (strcmp(loginValidado.usuario.login, login) == 0 && strcmp(loginValidado.usuario.senha, senha) == 0){
            loginValidado.validado = true;
            break;
        }
        else
            printf("Login inválido \n");   
            break; 
    }
    fclose(arquivoUsuario);
    return loginValidado;
}

void criarUsuario() {
    struct Usuario usuario;
    printf("Escolha o tipo de permissão do usuário\n");
    printf("2 - Gerente\n");
    printf("3 - Funcionário\n");
    scanf("%d", &usuario.permissao);
    printf("Digite o nome \n");
    fgets(usuario.nome, sizeof(usuario.nome), stdin);
    usuario.nome[strcspn(usuario.nome, "\n")] = '\0';
    printf("Digite o CPF \n");
    fgets(usuario.documento, sizeof(usuario.documento), stdin);
    usuario.documento[strcspn(usuario.documento, "\n")] = '\0';
    printf("Digite o login \n");
    fgets(usuario.login, sizeof(usuario.login), stdin);
    usuario.login[strcspn(usuario.login, "\n")] = '\0';
    printf("Digite a senha \n");
    fgets(usuario.senha, sizeof(usuario.senha), stdin);
    usuario.senha[strcspn(usuario.senha, "\n")] = '\0';

    abrirArquivoUsuarioLeitura();
    char line[100];
    while (fgets(line, sizeof(line), arquivoUsuario) != NULL) {
        fscanf(arquivoUsuario, "%s", line);
    }
    int id = line[0] - '0';
    usuario.id = id + 1;
    fecharArquivoUsuario();

    abrirArquivoUsuarioEscrita();
    gravarDadosEmArquivo(usuario);
    fecharArquivoUsuario();

    printf("\nUsuário cadastrado com sucesso.");
}

char* PermissaoString(int permissao) {
    switch (permissao)
    {
    case 1:
        return "Administrador";
        break;
    case 2:
        return "Gerente";
        break;
    case 3:
        return "Funcionário";
        break;
    }
}

void RelatorioUsuarios() {
    abrirArquivoUsuarioLeitura();
    struct Usuario *usuarios = NULL;
    char linha[50];
    int quantidade = 0;
    int capacidade = 2; 
    usuarios = (struct Usuario *)malloc(capacidade * sizeof(struct Usuario));
    if (usuarios == NULL) {
        printf("Erro ao alocar memória!\n");
    }

    while (!feof(arquivoUsuario))
    {
        if (quantidade == capacidade) {
            capacidade *= 2;
            usuarios = (struct Usuario *)realloc(usuarios, capacidade * sizeof(struct Usuario));
            if (usuarios == NULL) {
                printf("Erro ao realocar memória!\n");
            }
        }

        struct Usuario usuario;
        fscanf(arquivoUsuario,"%d,%[^,],%[^,],%d,%[^,],%[^\n]",&usuario.id, usuario.nome, usuario.documento, &usuario.permissao, usuario.login, usuario.senha);

        usuarios[quantidade] = usuario;
        quantidade++;
    }
    fclose(arquivoUsuario);

    for (int i = 0; i < quantidade; i++)
    {
        printf("Id: %d, Nome: %s, Documento: %s, Permissão: %s, Login: %s\n", 
            usuarios[i].id, usuarios[i].nome, usuarios[i].documento, PermissaoString(usuarios[i].permissao), usuarios[i].login);
    }
}

char** retornaOpcoesMenu(int permissao) {
    switch (permissao) {
        case 1:
            {
                return menuAdmin;
            }
        break;
        case 2:
            {
                return menuGerente;
            }
        break;
        case 3:
            {
                return menuFuncionario;
            }
        break;
    }
}

int retornaQtdOpcoes(int permissao) {
    if (permissao == 1)
        return 5;
    else if (permissao == 2)
        return 4;
    else
        return 1;  
}

void ManipularOpcaoSelecionadaAdmin(int opcaoEscolhida) {
    switch (opcaoEscolhida)
    {
    case 1:
        criarUsuario();
        break;
    case 2:
        cadastrarProduto();
        break;
    case 3:
        RelatorioUsuarios();
        break;
    case 4:
        exibirRelatorioVendas();
        break;
    case 5:
        relatorioProdutos();
        break;
    default:
        printf("Função Indisponível.");
        break;
    }
}

void ManipularOpcaoSelecionadaGerente(int opcaoEscolhida) {
    switch (opcaoEscolhida)
    {
    case 1:
        cadastrarProduto();
        break;
    case 2:
        exibirRelatorioVendas();
        break;
    case 3:
        relatorioProdutos();
        break;
    case 4:
        cadastrarVenda();
        break;
    default:
        printf("Função Indisponível.");
        break;
    }
}

void ManipularOpcaoSelecionadaFunc(int opcaoEscolhida) {
    switch (opcaoEscolhida)
    {
    case 1:
        cadastrarVenda();
        break;
    default:
        printf("Função Indisponível.");
        break;
    }
}