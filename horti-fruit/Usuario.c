#include <stdio.h>
#include <stdlib.h>
#include <_string.h>
#include <stdbool.h>
#include "Headers/Usuario.h"

const char *nome_arquivo_usuario = "usuario.txt";
FILE *arquivo = NULL;

struct Usuario {
    int id;
    char nome[20];
    char documento[20];
    int permissao;
    char login[10];
    char senha[5];
};

void abrirArquivoUsuarioEscrita() {
    arquivo = fopen(nome_arquivo_usuario, "a");
    if (arquivo == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void abrirArquivoUsuarioLeitura() {
    arquivo = fopen(nome_arquivo_usuario, "r");
    if (arquivo == NULL) {
        printf("O arquivo '%s' não existe. Criando o arquivo...\n", nome_arquivo_usuario);

        arquivo = fopen(nome_arquivo_usuario, "w");
        if (arquivo == NULL) {
            printf("Erro ao criar o arquivo.\n");
            return;    
        }
        fclose(arquivo);
        arquivo = fopen(nome_arquivo_usuario, "r");
        if (arquivo == NULL) {
            printf("Erro ao reabrir o arquivo em modo de leitura.\n");
            return;
        }
    }
}

void fecharArquivoUsuario() {
    if (arquivo != NULL) {
        fclose(arquivo);
        arquivo = NULL;
    }
}

bool validarUserAdmin() {
    abrirArquivoUsuarioLeitura();
    char linha[100];

    if (fgets(linha, sizeof(linha), arquivo) != NULL) {
        return true;
        fclose(arquivo);
    }
    else {
        return false;
        fclose(arquivo);
    }
}

void criarUsuarioAdmin() {
    char login[10];
    char senha[5];

    abrirArquivoUsuarioEscrita();
    printf("Parece que não existe nenhum usuário administrador cadastrado. \n");
    printf("Cadastre um administrador para efetuar o primeiro login no sistema. \n");
    printf("Digite o nome de usuário que será usado para fazer login \n");
    scanf("%s", &login);
    printf("Digite a senha \n");
    scanf("%s", &senha);
    fprintf(arquivo, "%s %s\n", login, senha);
    fecharArquivoUsuario();
}