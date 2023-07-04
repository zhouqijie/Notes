# 异步编程    

> Task非常适合异步编程，因为它们支持Continuation，这对异步非常重要。    

- **异步编程建议**    

1. I/O-Bound和ComputeBound操作建议用异步编写。  
2. 执行超过50毫秒的操作用异步编写。    
3. 粒度要适中。    


<br />
<br />


## `await`和`async`关键字    

### **`await`**    

`await`关键字简化了附加continuation的过程：  

```C#  
var result = await expression;
//等同于
var awaiter = expression.GetAwaiter();
awaiter.OnComplete(() => {
    var result = awaiter.GetResult();
});
```  


### **`async`**    

`async`修饰符会让编译器把`await`当作关键字而不是标识符。    

> async关键字只用用于方法。该方法可以返回void、Task、Task<>。    

### **异步函数执行**    

遇到`await`表达式，执行会返回调用者，就像`yield return`。  
在返回前，运行时会附加一个continuation到await的task。（await表达式由线程池中线程来计算）。    

> async异步函数被编译成一个状态机，实现语言运行时的协程。（yield也是状态机实现）    


### **await之后在哪个线程上执行**    

在富客户端应用上，同步上下文保证后续在原线程(UI线程)上执行。否则，就会在task结束的线程上继续执行。（ContinueWith()同）    



<br />
<br />

## 注意事项    

> 对任何异步函数，可以用Task替代void作为返回类型，让它可以await，并不需要显式返回Task，编译器会自动生成一个Task。    
> 方法体返回`TResult`，那么异步方法就可以返回`Task<TResult>`。其原理就是给TaskCompletion发送的信号带有值，而不是null。    

> 编译器会对返回Task的异步函数进行扩展，使其成为当发送信号或者发生故障时使用TCS来创建Task的代码。    



（END）    