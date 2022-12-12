namespace HareDu;

public interface ProtocolListenerConfigurator
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string Amqp091();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string Amqp10();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string Mqtt();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string Stomp();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string WebMqtt();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string WebStomp();
}