#ifndef PRODUTO.H
#define PRODUTO.H
#include <stdio.h>

struct Produto {
    int id;
    char nome[50];
    float valorProduto;
    float valorMinimo;
    int tipoProduto;
    int quantidadeEstoque;
};

void abrirArquivoProduto();
void fecharArquivoProduto();
void gravarDadosEmArquivoProduto(struct Produto produto);
void cadastrarProduto();
void estoqueProdutos();
void vendaProduto();
void adicionarEstoqueProduto();

#endif
