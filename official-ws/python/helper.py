# coding=utf-8

import json
import decimal
import datetime



# EPOCH = datetime.datetime.utcfromtimestamp(0)
#
#
# from collections import namedtuple
#
#
# def _json_object_hook(d):
#     return namedtuple('X', d.keys())(*d.values())
#
#
# def _json2obj(data):
#     return json.loads(data, object_hook=_json_object_hook)


class SupplementaryEncoder(json.JSONEncoder):
    def default(self, obj):
        from gmex_types import GT_Object
        if isinstance(obj, decimal.Decimal):  # for decimal
            # return str(obj)
            return float(obj)
        elif isinstance(obj, GT_Object):
            return obj.kv
        elif isinstance(obj, datetime.datetime):  # for datetime
            # return obj.strftime("%Y-%m-%d %H:%M:%S")
            # return (obj - EPOCH).total_seconds() * 1000
            return int(obj.timestamp())
        else:
            return json.JSONEncoder.default(self, obj)


def my_json_marshal(obj):
    return json.dumps(obj, ensure_ascii=False, sort_keys=True, separators=(',', ':'), cls=SupplementaryEncoder)


def my_json_unmarshal(txt):
    #return json.loads(txt, object_hook=_json_object_hook)
    return json.loads(txt, parse_float=decimal.Decimal)
