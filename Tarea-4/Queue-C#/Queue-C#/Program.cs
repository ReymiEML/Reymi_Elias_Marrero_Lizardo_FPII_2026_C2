using Queue_C_;

CQueue fila = new CQueue();

fila.Enqueue(5);
fila.Enqueue(3);
fila.Enqueue(7); 
fila.Enqueue(1);

fila.Transversa();

int valor = fila.Dequeue();
Console.WriteLine("El valor adquirido {0}", valor);
fila.Transversa();

fila.Enqueue(8);
fila.Transversa();

Console.WriteLine("El valor observado es {0}", fila.Peek());
fila.Transversa();