#ifndef VENDAS_H
#define VENDAS_H
#include <stdio.h>

struct Venda {
    int id;
    char docVendedor[20];
    char docCliente[20];
    float valorTotal;
};

void abrirArquivoVendas();
void fecharArquivoVendas();
void exibirRelatorioVendas();
void cadastrarVenda();

#endif