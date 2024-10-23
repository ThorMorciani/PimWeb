#include <stdio.h>
#include "Headers/Vendas.h"

const char *nome_arquivo_vendas = "vendas.txt";
FILE *arquivoVendas = NULL;

void abrirArquivoVendas() {
    arquivoVendas = fopen(nome_arquivo_vendas, "a");
    if (arquivoVendas == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void abrirArquivoVendasLeitura() {
    arquivoVendas = fopen(nome_arquivo_vendas, "r");
    if (arquivoVendas == NULL) {
        printf("O arquivo '%s' não existe. Criando o arquivo...\n", nome_arquivo_vendas);

        arquivoVendas = fopen(nome_arquivo_vendas, "wb");
        if (arquivoVendas == NULL) {
            printf("Erro ao criar o arquivo.\n");
            return;
        }
        fclose(arquivoVendas);
        arquivoVendas = fopen(nome_arquivo_vendas, "rb");
        if (arquivoVendas == NULL) {
            printf("Erro ao reabrir o arquivo em modo de leitura.\n");
            return;
        }
    }
}

void fecharArquivoVendas() {
    if (arquivoVendas != NULL) {
        fclose(arquivoVendas);
        arquivoVendas = NULL;
    }
}
