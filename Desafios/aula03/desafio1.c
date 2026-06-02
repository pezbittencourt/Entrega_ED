#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#define REBELDE 1
#define IMPERIO 2

#define MAX_NOME 50
#define MAX_CLASSE 50
#define MAX_HABILIDADE 50

typedef struct habilidade {
    char nome[MAX_HABILIDADE];
    float modificador;
} Habilidade;

typedef struct personagem {
    char nome[MAX_NOME];
    int tipo;
    char classe[MAX_CLASSE];
    int vida;
    int dano;
    int iniciativa;
    int temHabilidade;
    Habilidade habilidade;
} Personagem;

typedef struct no {
    Personagem personagem;
    struct no *prox;
} No;

typedef No *Celula;

typedef struct lista {
    Celula inicio;
    Celula fim;
} Lista;

typedef Lista *ListaCircular;


ListaCircular novaListaCircular(void) {
    ListaCircular lista = (ListaCircular) malloc(sizeof(Lista));

    lista->inicio = NULL;
    lista->fim = NULL;

    return lista;
}

Celula novaCelula(Personagem p) {
    Celula nova = (Celula) malloc(sizeof(No));

    nova->personagem = p;
    nova->prox = NULL;

    return nova;
}

int listaVazia(ListaCircular lista) {
    return lista == NULL || lista->inicio == NULL;
}

void insereOrdenadoPorIniciativa(ListaCircular lista, Personagem p) {

    Celula nova = novaCelula(p);

    if (lista->inicio == NULL) {
        lista->inicio = nova;
        lista->fim = nova;
        nova->prox = nova;
        return;
    }

    if (p.iniciativa < lista->inicio->personagem.iniciativa) {
        nova->prox = lista->inicio;
        lista->inicio = nova;
        lista->fim->prox = lista->inicio;
        return;
    }

    if (p.iniciativa >= lista->fim->personagem.iniciativa) {
        nova->prox = lista->inicio;
        lista->fim->prox = nova;
        lista->fim = nova;
        return;
    }

    Celula aux = lista->inicio;

    while (aux->prox != lista->inicio &&
           aux->prox->personagem.iniciativa <= p.iniciativa) {
        aux = aux->prox;
    }

    nova->prox = aux->prox;
    aux->prox = nova;
}

void printListaCircular(ListaCircular lista) {

    if (listaVazia(lista)) {
        printf("[Lista vazia]\\n");
        return;
    }

    Celula aux = lista->inicio;

    do {

        printf("%s{%s, Classe=%s, HP=%d, D=%d, Ini=%d}",
               aux->personagem.nome,
               aux->personagem.tipo == REBELDE ? "Rebelde" : "Imperio",
               aux->personagem.classe,
               aux->personagem.vida,
               aux->personagem.dano,
               aux->personagem.iniciativa);

        if (aux->personagem.temHabilidade) {
            printf("[Hab=%s x%.1f]",
                   aux->personagem.habilidade.nome,
                   aux->personagem.habilidade.modificador);
        }

        aux = aux->prox;

        if (aux != lista->inicio)
            printf(" -> ");

    } while (aux != lista->inicio);

    printf("\\n");
}


Celula buscaInimigoMaisProximo(Celula atual) {

    Celula aux = atual->prox;

    do {

        if ((atual->personagem.tipo == REBELDE &&
             aux->personagem.tipo == IMPERIO)

            ||

            (atual->personagem.tipo == IMPERIO &&
             aux->personagem.tipo == REBELDE)) {

            return aux;
        }

        aux = aux->prox;

    } while (aux != atual->prox);

    return NULL;
}

void removeDaListaCircular(ListaCircular lista, Celula alvo) {

    if (listaVazia(lista) || alvo == NULL)
        return;

    if (lista->inicio == alvo &&
        lista->fim == alvo) {

        free(alvo);

        lista->inicio = NULL;
        lista->fim = NULL;

        return;
    }

    Celula aux = lista->inicio;

    while (aux->prox != alvo)
        aux = aux->prox;

    aux->prox = alvo->prox;

    if (alvo == lista->inicio)
        lista->inicio = alvo->prox;

    if (alvo == lista->fim)
        lista->fim = aux;

    lista->fim->prox = lista->inicio;

    free(alvo);
}

double numeroAleatorio(void) {
    return (double) rand() / RAND_MAX;
}

Celula executaUmTurno(ListaCircular lista, Celula atual) {

    Celula inimigo = buscaInimigoMaisProximo(atual);

    if (inimigo == NULL)
        return atual->prox;

    Celula proximo = atual->prox;

    double dano = atual->personagem.dano;

    if (atual->personagem.temHabilidade &&
        numeroAleatorio() <= 0.20) {

        dano *= atual->personagem.habilidade.modificador;

        printf("\\n%s usou %s!\\n",
               atual->personagem.nome,
               atual->personagem.habilidade.nome);
    }

    inimigo->personagem.vida -= (int)dano;

    printf("%s atacou %s causando %.0f de dano!\\n",
           atual->personagem.nome,
           inimigo->personagem.nome,
           dano);

    if (inimigo->personagem.vida <= 0) {

        printf("%s foi derrotado!\\n",
               inimigo->personagem.nome);

        removeDaListaCircular(lista, inimigo);
    }

    return proximo;
}


int main(void) {

    srand(time(NULL));

    ListaCircular batalha = novaListaCircular();

    Personagem luke = {
        "Luke Skywalker",
        REBELDE,
        "Jedi",
        30,
        8,
        5,
        1,
        {"Golpe de Sabre", 2.0f}
    };

    Personagem leia = {
        "Leia Organa",
        REBELDE,
        "General Rebelde",
        24,
        6,
        8,
        1,
        {"Tiro Preciso", 1.5f}
    };

    Personagem vader = {
        "Darth Vader",
        IMPERIO,
        "Sith",
        40,
        10,
        6,
        1,
        {"Forca Sombria", 2.5f}
    };

    Personagem stormtrooper = {
        "Stormtrooper",
        IMPERIO,
        "Soldado Imperial",
        18,
        5,
        3,
        0,
        {"", 0.0f}
    };

    insereOrdenadoPorIniciativa(batalha, luke);
    insereOrdenadoPorIniciativa(batalha, leia);
    insereOrdenadoPorIniciativa(batalha, vader);
    insereOrdenadoPorIniciativa(batalha, stormtrooper);

    printf("=== BATALHA STAR WARS ===\\n\\n");

    printListaCircular(batalha);

    Celula atual = batalha->inicio;

    int turno;

    for (turno = 1; turno <= 10; turno++) {

        printf("\\n========== TURNO %d ==========\\n",
               turno);

        atual = executaUmTurno(batalha, atual);

        printListaCircular(batalha);
    }

    return 0;
}