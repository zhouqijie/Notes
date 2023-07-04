


# CRE：渲染总结概览      


```CPP  
=>Application::UpdateScene()

    => if (updateWorld) PlayerLoop() // player loop 
        => RenderManager::UpdateAllRenderers();
        => if !UNITY_EDITOR  
                => if(!batchmode)PlayerRender()//player renderer
                    => RenderManager::RenderOffscreenCameras();
	                => RenderManager::RenderCameras();

    => if (frame.GetSuccess()) RenderManager::RenderOffscreenCameras() (应该是单例模式，调用了GetRenderManager()函数)

            =>Camera.DoRender()

                =>DoRendererLoop()   
                        =>⭕if(renderPath == kRenderPathPrePass)  
                            DoPrePassRenderLoop()

                                            =>ForwardShaderRenderLoop queue
                                            =>ForwardShaderRenderLoop::PerformRendering
                                                    =>for(;passes;)
                                                        =>for(;;)
                                                            => VisibleNode * node  
                                                            => 要么node->Render()
                                                            => 或者m_BatchRenderer.Add  (node->renderer ,....

                        =>⭕if(renderPath == kRenderPathForward)
                            DoForwardShaderRenderLoop()

                        =>⭕else 
                            DoForwardVertexRenderLoop()  


                                            =>ForwardVertexRenderLoop queue;
                                            =>设置queue.m_Objects(RenderObjectDataContainer)  
                                            => queue.PerformRendering()

                                                =>for(;;)

                                                    =>VisibleNode * node
                                                    =>ForwardVertexRenderLoop->m_BatchRenderer. Add                                     (node->renderer , ...)  

                                                            =>BatchRenderer::Add
                                                                =>要么：m_BatchInstances.pushback   () //加入容器？？
                                                                =>或者：renderer-Render() 直接渲    染？？
```                                                                  


## 如何选择要渲染的Renderer？    


？？Camera::CustomCull ？？
    ？？PrepareSceneCullingParameters    >>   ？？SceneCullParameters？？实际的场景Renderer选取??  
            sceneCullParameters.renderers[kStaticRenderers].nodes = GetScene().GetStaticSceneNodes();
            sceneCullParameters.renderers[kCameraIntermediate].nodes = cameraIntermediate.GetSceneNodes();
                返回Scene::m_RendererNodes.begin()  
    ？？CullScene？？
            ？？PrepareSceneNodes？？填充VisObjs??  




## Scene.m_RendererNodes来自何处？    

Scene::AddRendererInternal(Renderer*)  

    Scene::AddRenderer  

        Renderer::UpdateRenderer ()



（END）  