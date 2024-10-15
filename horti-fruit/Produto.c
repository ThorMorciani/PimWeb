#include <stdio.h>
#include "Headers/Produto.h"

const char *nome_arquivo_produto = "produto.txt";
FILE *arquivo = NULL;

struct Produto {
    int id;
    char nome[50];
    float valorProduto;
    float valorMinimo;
    int tipoProduto;
    int quantidadeEstoque;
};

void abrirArquivoProduto() {
    arquivo = fopen(nome_arquivo_produto, "a");
    if (arquivo == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void fecharArquivoProduto() {
    if (arquivo != NULL) {
        fclose(arquivo);
        arquivo = NULL;
    }
}