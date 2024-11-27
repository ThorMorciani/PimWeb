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
void abrirArquivoProdutoLeitura();
void fecharArquivoProduto();
void gravarDadosEmArquivoProduto(struct Produto produto);
void cadastrarProduto();
void estoqueProdutos();
void vendaProduto();
void adicionarEstoqueProduto();
void alterarValorProduto();
void excluirProduto();
struct Produto buscarProdutoPorId(int id);
void removerEstoqueProduto(int id, int qtdVentida);
void relatorioProdutos();

#endif
