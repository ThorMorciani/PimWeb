#include <stdio.h>
#include "Headers/Produto.h"

const char *nome_arquivo_produto = "produto.txt";
FILE *arquivoProduto = NULL;

void abrirArquivoProduto() {
    arquivoProduto = fopen(nome_arquivo_produto, "a");
    if (arquivoProduto == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void abrirArquivoProdutoLeitura() {
    arquivoProduto = fopen(nome_arquivo_produto, "r");
    if (arquivoProduto == NULL) {
        printf("O arquivo '%s' não existe. Criando o arquivo...\n", nome_arquivo_produto);

        arquivoProduto = fopen(nome_arquivo_produto, "wb");
        if (arquivoProduto == NULL) {
            printf("Erro ao criar o arquivo.\n");
            return;    
        }
        fclose(arquivoProduto);
        arquivoProduto = fopen(nome_arquivo_produto, "rb");
        if (arquivoProduto == NULL) {
            printf("Erro ao reabrir o arquivo em modo de leitura.\n");
            return;
        }
    }
}

void fecharArquivoProduto() {
    if (arquivoProduto != NULL) {
        fclose(arquivoProduto);
        arquivoProduto = NULL;
    }
}