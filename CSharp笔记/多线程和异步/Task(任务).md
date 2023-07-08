# 任务(Task)    

> 命名空间：`using System.Threading.Task`;
> Task可以在操作完成后返回一个值，而线程池不可以。    
> 长时间运行的任务或操作，可以用Long-Running Task。    

> Task的异常会抛送给task.Result或者task.Wait()方法处。    


### 创建Task并执行    

```C#  
Task task = new Task(Foo);
task.Start();//冷任务

//或者
Task.Run();//热任务  (.net 4.5)
```  

调用`Wait()`方法会进行阻塞直到操作完成，相当于thread的`Join()`方法。    


### Task.Result    

> 从Task返回一个结果值，访问Result属性时，task如果没有完成则会阻塞线程直到完成。    

### Task.ContinueWith    

> 使用awaiter对象，或者使用Task.ContinueWith方法。(.NET4以上)    

```C#  
Task<int> t1 = new Task<int>(Foo); t1.Start();
Task t2 = t.ContinueWith(t => t.Console.WriteLine(t.Result));//在原task的线程上继续执行。  
```  

<br />  
<br />  
<br />  
<br />  


# Task进阶    


## Awaiter对象    

获得Awaiter对象：  
`var awaiter = task.GetAwaiter();`  
结束/故障回调：  
`awaiter.OnComplete(delegate);`    
获得Task返回值：  
`var result = awaiter.GetResult();`    

```C#  
var awaiter =  task.GetAwaiter();  
awaiter.OnComplete(() => {
    var result = awaiter.GetResult();
    Console.WriteLine(result);  
});
```  



## TaskCompletionSource    

> TaskCompletionSource是一种受你控制创建Task的方式。你可以使Task在任何你想要的时候完成。你也可以在任何地方给它一个异常让他失败。    

使用示例：  
```C#  
        static void Main(string[] args)
        {
            Task<int> task = Run<int>(() => { return 999; });
            task.GetAwaiter().OnCompleted(() => {
                Console.WriteLine("Result: " + task.Result);
            });
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("main:" + i);
            }
        }

        static Task<int> Run<TResutl>(Func<int> function)
        {
            var tcs = new TaskCompletionSource<int>();
            new Thread(() => {
                Thread.Sleep(5);
                try
                {
                    tcs.SetResult(function());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }

            }).Start();
            return tcs.Task;
        }
```  


<br />  
<br />  
<br />  
<br />  


（END）    


