import pika

hostname = "localhost"
queue_name = "exampleQueue"

connection = pika.BlockingConnection(pika.ConnectionParameters(host=hostname))
channel = connection.channel()
channel.queue_declare(queue=queue_name)

def callback(ch, method, properties, body):
    print("Received message:", body)

channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)

print('Waiting for messages')
channel.start_consuming()