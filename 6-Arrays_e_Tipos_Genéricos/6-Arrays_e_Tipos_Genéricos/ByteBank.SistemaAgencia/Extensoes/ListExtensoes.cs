﻿namespace ByteBank.SistemaAgencia.Extensoes
{
    internal static class ListExtensoes
    {

        public static void AdicionarVarios<T>(this List<T> lista, params T[] itens)
        {
            foreach (T item in itens)
            {
                lista.Add(item);
            }
        }

    }
}
