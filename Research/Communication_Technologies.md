# Communication technologies

In order to reliably receive and send information to and from the dashboard we need to look into which possible technologies exist in order to make communication between the 2 possible. When we research these possible technologies we have to keep the following constraints/requirements in mind:
- Protocol/technology needs to be lightweight because of the limited resources available on the hardware side of the control points.
- Protocol needs to establish communication between connected devices and a gateway.
- Protocol needs to communicate over Wi-Fi.

This leaves us with a few options for possible protocols to use: 
- MQTT
- RabbitMQ
- Kafka
- ZeroMQ
- gRPC

## Research question
Which one of the abovementioned communication protocols would be best suited for the needs of our IoT system?

## Research sub-questions
1. What is a pub/sub messaging pattern and is it beneficial to choose this over other types of patterns?
2. What are the benefits and downsides when comparing the relevant messaging protocols to each other?
3. Which of the aforementioned protocols can be used in a LAN setup where no further internet connection is needed?

## Methods
Now that we have formulated our research question and our sub-questions we can start choosing DOT framework research methods that are suitable for our questions. The ones that seem most suitable are literature study for both questions as there is a lot of documentation available online regarding relevant use-cases, comparisons and limitations.

## Answers
### 1. Pub/Sub messaging pattern
Publish/subscribe messaging, or pub/sub messaging, is a form of asynchronous service-to-service communication used in serverless and microservices architectures. In a pub/sub model, any message published to a topic is immediately received by all of the subscribers to the topic<sup>[1](##Sources)</sup>.

This pattern is extremely attractive for IoT, because for instance, it eliminates polling. Our system relies on real-time events to function and display changes. Message topics allow for instantaneous, push-based delivery, eliminating the need for message consumers to periodically check or “poll” for new information and updates.<sup>[2](##Sources)</sup>

Furthermore, pub/sub makes discovery of services easier, more natural and less error prone. Instead of maintaining a roster of peers that an application can send messages to, a publisher will simply post messages to a topic. Then, any interested party will subscribe its endpoint to the topic, and start receiving these messages. Subscribers can change, upgrade, multiply or disappear and the system dynamically adjusts.<sup>[2](##Sources)</sup>

Lastly, pub/Sub makes software more flexible. Publishers and subscribers are decoupled and work independently from each other, which allows you to develop and scale them independently. You can decide to handle orders one way this month, then a different way next month. Adding or changing functionality won’t send ripple effects across the system.<sup>[2](##Sources)</sup>

In conclusion, using a communication protocol that uses the pub/sub messaging pattern has a lot of benefits that are useful for our use-case and therefore we'll choose for this type of protocol. This leaves us with the following protocols to look at:

- MQTT
- RabbitMQ
- Kafka

### 2. Protocol comparison
The table below shows a direct comparison of various aspects of all 3 systems pulled from [simplilearn](https://www.simplilearn.com/kafka-vs-rabbitmq-article).
<table>
  <thead>
    <tr>
      <th></th>
      <th>Kafka</th>
      <th>RabbitMQ</th>
      <th>MQTT</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Type</th>
      <td>Message broker</td>
      <td>Message broker</td>
      <td>Messaging protocol</td>
    </tr>
    <tr>
      <th>LAN Integration</th>
      <td>Yes, dedicated server.</td>
      <td>Yes, dedicated server.</td>
      <td>Yes, broker can be integrated into application directly.</td>
    </tr>
    <tr>
      <th>Targeted use-case</th>
      <td>High-ingress data streams with replay capability.</td>
      <td>General-purpose messaging with support for many protocols.</td>
      <td>IoT optimized messaging protocol that allows for millions of connections.</td>
    </tr>
    <tr>
      <th>Performance</th>
      <td>1 million messages per second</td>
      <td>4K-10K messages per second</td>
      <td>100K messages per second theoretical max</td>
    </tr>
    <tr>
      <th>Message Retention</th>
      <td>Policy-based (e.g., 30 days)</td>
      <td>Acknowledgment based</td>
      <td>Acknowledgment based</td>
    </tr>
    <tr>
      <th>Data Type</th>
      <td>Operational (process operations, auditing and logging statistics, and system activity)</td>
      <td>Transactional (order formation and placement, and user requests)</td>
      <td>Transactional (order formation and placement, and user requests)</td>
    </tr>
    <tr>
      <th>Payload Size</th>
      <td>Default 1MB limit</td>
      <td>No constraints</td>
      <td>260MB</td>
    </tr>
  </tbody>
</table>

As can be seen Kafka is way more suited for high quantity data streaming with replay functionality and message retention whilst RabbitMQ and MQTT are more suited to our requirements.

### 3. LAN setup
All 3 options can be integrated into a LAN environment, so we're going to take a deeper dive into which one would be the easiest to integrate into our application so that it can be used on the local private network we have at the location. MQTT allows for the use of a local broker that can be integrated into the application as a hosted service. For both RabbitMQ and Kafka it would be required to run a seperate application/instance on which the broker or cluster for communication can be run.

## Conclusion
MQTT, RabbitMQ and Kafka are all excellent choices for Pub/Sub messaging protocols, but in terms of convenience MQTT is the winner. It allows for easy integration with the main application and ticks all the boxes in terms of required features.

## Sources
1. [What is pub/sub messaging? (n.d.). Amazon Web Services, Inc. Retrieved June 2, 2022](https://aws.amazon.com/pub-sub-messaging/)
2. [ What are Benefits of Pub/Sub Messaging? – AWS. (n.d.). Amazon Web Services, Inc. Retrieved June 2, 2022](https://aws.amazon.com/pub-sub-messaging/benefits/)