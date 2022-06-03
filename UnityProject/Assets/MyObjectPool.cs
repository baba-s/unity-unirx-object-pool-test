using UniRx.Toolkit;
using UnityEngine;

// オブジェクトプールを管理するクラス
public sealed class MyObjectPool : ObjectPool<MyObject>
{
    private readonly MyObject m_prefab; // プレハブ

    // コンストラクタ
    public MyObjectPool( MyObject prefab )
    {
        m_prefab = prefab;
    }

    // プールにオブジェクトが不足している時に
    // オブジェクトを生成するために呼び出されます
    protected override MyObject CreateInstance()
    {
        // オブジェクトを生成します
        return Object.Instantiate( m_prefab );
    }

    // Rent 関数でプールからオブジェクトを取得する時に呼び出されます
    protected override void OnBeforeRent( MyObject instance )
    {
        // プールから取得したオブジェクトをアクティブにします
        instance.gameObject.SetActive( true );

        // オブジェクトの位置をランダムに設定します
        const float range = 3f;
        instance.transform.position = new Vector3
        (
            x: Random.Range( -range, range ),
            y: Random.Range( -range, range ),
            z: Random.Range( -range, range )
        );
    }

    // Return 関数でプールにオブジェクトを戻す時に呼び出されます
    protected override void OnBeforeReturn( MyObject instance )
    {
        // プールに戻すオブジェクトは非アクティブにします
        instance.gameObject.SetActive( false );
    }

    // Clear 関数でプールに存在するオブジェクトを削除する時に呼び出されます
    protected override void OnClear( MyObject instance )
    {
        if ( instance == null ) return;
        if ( instance.gameObject == null ) return;

        // プールに存在するオブジェクトを削除します
        Object.Destroy( instance.gameObject );
    }
}