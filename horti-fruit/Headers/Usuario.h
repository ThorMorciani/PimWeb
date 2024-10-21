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

void abrirArquivoUsuarioEscrita();
void abrirArquivoUsuarioLeitura();
void fecharArquivoUsuario();
bool validarUserAdmin();
void criarUsuarioAdmin();
struct LoginValidado ValidaLogin(char login[10], char senha[10]);

#endif
