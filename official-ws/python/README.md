# gmex-api-python

当前代码完成了 websocket 的封装，Model 类还没找到合适的py解决方案，暂时手写
了Order和Postion的封装，后继看有啥好的方法重写，暂时建议大家直接用dict参考
文档看各个字段的意思吧。
通常各自使用的技术栈都有各种model方案，请大家推荐好的给我。

REST 部分暂时没有示例，其都是简单的http post请求，参考js或dotnet的实现应该
很容易自己搞定。

有啥好的建议和想法，请email我<hexiaoyuan@126.com>

==========
pipenv --three
pipenv install --dev
pipenv update

pipenv shell

python main.py

==========
