#ifndef USUARIO.H
#define USUARIO.H 
#include <stdio.h>
#include <stdlib.h>
#include <_string.h>
#include <stdbool.h>

void abrirArquivoUsuarioEscrita();
void abrirArquivoUsuarioLeitura();
void fecharArquivoUsuario();
bool validarUserAdmin();
void criarUsuarioAdmin();

#endif
