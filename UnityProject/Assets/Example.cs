using System.Threading.Tasks;
using UnityEngine;

public sealed class Example : MonoBehaviour
{
    [SerializeField] private MyObject m_prefab; // プレハブ

    private MyObjectPool m_pool; // オブジェクトプール

    private void Start()
    {
        // オブジェクトプールを作成します
        m_pool = new MyObjectPool( m_prefab );
    }

    private async void OnGUI()
    {
        GUILayout.Label( $"プールされている非アクティブなオブジェクトの数：{m_pool.Count.ToString()}" );

        if ( GUILayout.Button( "生成" ) )
        {
            // プールからオブジェクトを取得します
            var enemy = m_pool.Rent();

            // 2 秒後に
            await Task.Delay( 2000 );

            // オブジェクトをプールに戻します
            m_pool.Return( enemy );
        }
        else if ( GUILayout.Button( "プールをクリア" ) )
        {
            // プールに存在する非アクティブなオブジェクトをすべて削除します
            m_pool.Clear();
        }
    }
}