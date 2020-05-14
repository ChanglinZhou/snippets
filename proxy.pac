var proxy = "SOCKS5 10.149.5.2:8080; SOCKS 10.149.5.2:8080; DIRECT;";

var direct = 'DIRECT;';

var ipv4 = /^(?!0)(?!.*\.$)((1?\d?\d|25[0-5]|2[0-4]\d)(\.|$)){4}$/

function FindProxyForURL(url, host) {
  var knowHosts = [
   "host-dmhub.ccic-net.com.cn"
  ]
  // var proxy = "PROXY http://10.149.5.2:8080; DIRECT;";
  var proxy = "PROXY 10.149.5.2:8080; DIRECT;"
  var direct = 'DIRECT;';
  
  if (host == 'localhost') return direct;
  if (ipv4.test(host) && host.startsWith("10.")) return direct;
  if (knownHosts.indexOf(host) > -1) return proxy;
  if (host.endsWith('.ccic-net.com.cn') ||
      host.endsWith('.ccic-test.com.cn'))
      return direct;
  return proxy;
}
