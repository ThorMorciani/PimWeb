#include <stdio.h>
#include "Headers/Vendas.h"

const char *nome_arquivo_vendas = "vendas.txt";
FILE *arquivo = NULL;

struct Venda {
    int id;
    char docVendedor[20];
    char docCliente[20];
    float valorTotal;
};

void abrirArquivoVendas() {
    arquivo = fopen(nome_arquivo_vendas, "a");
    if (arquivo == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void fecharArquivoVendas() {
    if (arquivo != NULL) {
        fclose(arquivo);
        arquivo = NULL;
    }
}