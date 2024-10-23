#ifndef PRODUTO_H
#define PRODUTO_H

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

struct Produto {
    int id;
    char nome[50];
    float valorProduto;
    float valorMinimo;
    int tipoProduto;
    int quantidadeEstoque;
};

void abrirArquivoProdutoEscrita();
void abrirArquivoProdutoLeitura();
void fecharArquivoProduto();
void gravarDadosEmArquivoProduto(struct Produto produto);
void cadastrarProduto();
void estoqueProdutos();

#endif
