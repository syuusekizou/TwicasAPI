<?php
$clientId = '108565848.a9fe34aabdd7038216f7526847919343ea9fcf6887e8b21c9477aada5b57a5fb';
$clientSecret = 'cc6f1017b9fff42be61d2338917726850f706cdf8817a08ccaa00520fea06b0e';
$callbackUrl = 'http://fumyuun.php.xdomain.jp/token-comentbot.php';
$state = 'testdengana';

session_start();

// 最初に開いたとき、セッションにCSRF対策トークンを入れてツイキャスの認可画面に飛ばす
if (!isset($_GET['state'])) {
    $_SESSION['state'] = $state;
    $authorizationUrl = "https://apiv2.twitcasting.tv/oauth2/authorize?client_id=${clientId}&response_type=code&state=${state}";
    header("Location: ${authorizationUrl}");
    exit;
}

// コールバックで戻ってきたとき、CSRF対策としてstateを検証する
if ($_SESSION['state'] != $_GET['state']) {
    exit;
}

// アクセストークン取得APIで必要なパラメーター
$params = array(
    'code' => $_GET['code'], // コールバックで受け取ったcodeが必要
    'grant_type' => 'authorization_code',
    'client_id' => $clientId,
    'client_secret' => $clientSecret,
    'redirect_uri' => $callbackUrl,
);
$data = http_build_query($params);

$curl = curl_init();
curl_setopt($curl, CURLOPT_URL, 'https://apiv2.twitcasting.tv/oauth2/access_token');
curl_setopt($curl, CURLOPT_CUSTOMREQUEST, 'POST'); // post
curl_setopt($curl, CURLOPT_POSTFIELDS, $data); 
curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
curl_setopt($curl, CURLOPT_HEADER, false);

$result = curl_exec($curl);
curl_close($curl);

$json = json_decode($result);
?>

<html>
<style type='text/css'>
    <!--
    #body {
        width: 80%;
        word-break: break-word;
    }
    -->
</style>
<body>
    <div id="body">
        <ul>
            <li>token_type<br><?php echo $json->token_type; ?></li>
            <li>expires_in(トークンが失効するまでの秒数)<br><?php echo $json->expires_in; ?></li>
            <li>access_token(アクセストークン)<br><?php echo $json->access_token; ?></li>
            <br><br>＊上記access_token(アクセストークン)の値を<br>「config.json」の「access_token_bearer」に追記
        </ul>
    </div>
</body>
</html>