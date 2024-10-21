#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include "Headers/Usuario.h"

const char *nome_arquivo_usuario = "usuario.txt";
FILE *arquivoUsuario = NULL;

// struct Usuario {
//     int id;
//     char nome[20];
//     char documento[20];
//     int permissao;
//     char login[10];
//     char senha[5];
// };

// struct LoginValidado {
//     struct Usuario usuario;
//     bool validado;
// };

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
    fprintf(arquivoUsuario, "%d,%s,%s,%d,%s,%s", usuario.id, usuario.nome, usuario.documento, usuario.permissao, usuario.login, usuario.senha);
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
    printf("Digite o nome de usuário que será usado para fazer login \n");
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
        if (strcmp(loginValidado.usuario.login, login) == 0 && strcmp(loginValidado.usuario.senha, senha) == 0)
            loginValidado.validado = true;
        else
            printf("Login inválido \n");    
    }
    fclose(arquivoUsuario);
    return loginValidado;
}

// void RetornaMenu(int permissao) {
//     switch (permissao)
//     {
//     case 1:
//         return char menus = ["1 - Cadastrar Funcionário", "2 - Relatorio de Usuarios", "3 - Relatório de vendas"];
//         break;
    
//     default:
//         break;
//     }
// }